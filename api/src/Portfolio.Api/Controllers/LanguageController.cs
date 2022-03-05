using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Domain.Dtos.Languages;
using Portfolio.Domain.Extensions;
using Portfolio.Domain.Models.Localization;
using Portfolio.Domain.Wrapper;
using Portfolio.Services.Common;
using Portfolio.Services.Languages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class LanguageController : ControllerBase
{
    #region Fields

    private readonly ILanguageService _languageService;
    private readonly IUploadImageHelper _uploadImageHelper;
    private readonly IMapper _mapper;

    #endregion

    #region Constructor

    public LanguageController(ILanguageService languageService, IUploadImageHelper uploadImageHelper, IMapper mapper)
    {
        _languageService = languageService;
        _uploadImageHelper = uploadImageHelper;
        _mapper = mapper;
    }

    #endregion

    #region Utils

    private async Task<string> ValidateCreateUpdate(LanguageBaseCreateUpdateDto dto, int? id = null)
    {
        if (dto == null)
            return "Unkown error";

        if (string.IsNullOrEmpty(dto.Name))
            return "Please enter a language name";

        if (string.IsNullOrEmpty(dto.LanguageCulture))
            return "Please enter the language culture type";

        if (!CultureInfo.GetCultures(CultureTypes.AllCultures)
            .Any(culture => string.Equals(culture.Name, dto.LanguageCulture, StringComparison.CurrentCultureIgnoreCase)))
            return "There is no culture with the provided type";

        if (await _languageService.IsExistingNameOrCulture(dto.Name, dto.LanguageCulture, id ?? 0))
            return "The language name or culture is already used";

        return "";
    }

    #endregion

    #region Methods

    #region Get

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var languages = await _languageService.GetAllAsync();
        if (languages == null)
            return Ok(Result.FailAsync($"No languages found"));

        var languagesResult = languages.ToListResult();

        var result = _mapper.Map<ListResult<LanguageDto>>(languagesResult);
        result.Succeeded = true;

        return Ok(result);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(int id)
    {
        var language = await _languageService.GetByIdAsync(id);
        if (language == null)
            return Ok(Result.FailAsync($"No language found with id: {id}"));

        return Ok(Result<LanguageDto>.SuccessAsync(_mapper.Map<LanguageDto>(language)));
    }

    #endregion

    #region Post

    [HttpPost("Search")]
    public async Task<IActionResult> Search(LanguageSearchDto dto)
    {
        var languages = await _languageService.GetAllAsync(pageIndex: dto.Page - 1, pageSize: dto.PageSize);
        var model = await new LanguageListDto().PrepareToGridAsync(dto, languages, () =>
        {
            return languages.ToAsyncEnumerable().SelectAwait(async language => _mapper.Map<LanguageDto>(language));
        });

        var result = await Result<LanguageListDto>.SuccessAsync(model);
        return Ok(result);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(LanguageCreateDto languageDto)
    {
        var error = await ValidateCreateUpdate(languageDto);
        if (!string.IsNullOrEmpty(error))
            return Ok(await Result.FailAsync(error));
        
        var language = _mapper.Map<Language>(languageDto);
        await _languageService.InsertAsync(language);

        return Ok(await Result<LanguageDto>.SuccessAsync(_mapper.Map<LanguageDto>(language)));
    }

    [HttpPost("{languageId}/UploadLanguageIcon")]
    public async Task<IActionResult> UploadLanguageIcon(int languageId, IFormFile file)
    {
        var language = await _languageService.GetByIdAsync(languageId);
        if(language == null)
            return Ok(await Result.FailAsync("No language found with the provided id"));

        if (file == null)
            return Ok(await Result.FailAsync("No image provided"));

        //Validate image file
        var errorMessage = _uploadImageHelper.ValidateImage(file);
        if (!string.IsNullOrEmpty(errorMessage))
            return Ok(await Result.FailAsync(errorMessage));

        //Save the image file.
        var imagePath = await _uploadImageHelper.UploadLanguageFlagImageAsync(file);

        //Update the language
        language.FlagImageFilePath = imagePath;
        await _languageService.UpdateAsync(language);

        return Ok(await Result<LanguageDto>.SuccessAsync(_mapper.Map<LanguageDto>(language)));
    }

    #endregion

    #region Put

    [HttpPut]
    public async Task<IActionResult> Update(LanguageUpdateDto languageDto)
    {
        var error = await ValidateCreateUpdate(languageDto, languageDto.Id);
        if (!string.IsNullOrEmpty(error))
            return Ok(await Result.FailAsync(error));

        var language = await _languageService.GetByIdAsync(languageDto.Id);
        if(language == null)
            return Ok(await Result.FailAsync("No language found"));

        language = _mapper.Map(languageDto, language);
        await _languageService.UpdateAsync(language);

        return Ok(await Result<LanguageDto>.SuccessAsync(_mapper.Map<LanguageDto>(language)));
    }

    #endregion

    #region Delete

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var language = await _languageService.GetByIdAsync(id);
        if (language == null)
            return Ok(await Result.FailAsync("Language not found"));

        await _languageService.DeleteAsync(language);
        return Ok(await Result.SuccessAsync("Language removed"));
    }

    #endregion

    #endregion
}
