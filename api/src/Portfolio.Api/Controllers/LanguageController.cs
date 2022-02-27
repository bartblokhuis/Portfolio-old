using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Domain.Dtos.Languages;
using Portfolio.Domain.Extensions;
using Portfolio.Domain.Models.Localization;
using Portfolio.Domain.Wrapper;
using Portfolio.Services.Languages;
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
    private readonly IMapper _mapper;

    #endregion

    #region Constructor

    public LanguageController(ILanguageService languageService, IMapper mapper)
    {
        _languageService = languageService;
        _mapper = mapper;
    }

    #endregion

    #region Utils

    private async Task<string> ValidateCreateUpdate(LanguageBaseDto dto, int? id = null)
    {
        if (dto == null)
            return "Unkown error";

        if (string.IsNullOrEmpty(dto.Name))
            return "Please enter a language name";

        if (string.IsNullOrEmpty(dto.LanguageCulture))
            return "Please enter the language culture type";

        if (await _languageService.IsExistingNameOrCulture(dto.Name, dto.LanguageCulture, id ?? 0))
            return "The language name or culture is already used";

        return "";
    }

    #endregion

    #region Methods

    #region Get

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

        return Ok(Result<LanguageDto>.SuccessAsync(_mapper.Map<LanguageDto>(language)));
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

        return Ok(Result<LanguageDto>.SuccessAsync(_mapper.Map<LanguageDto>(language)));
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
