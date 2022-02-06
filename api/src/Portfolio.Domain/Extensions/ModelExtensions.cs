using Portfolio.Domain.Dtos.Common;
using Portfolio.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Domain.Extensions;

public static class ModelExtensions
{
    public static async Task<TListModel> PrepareToGridAsync<TListModel, TModel, TObject>(this TListModel listModel,
            BaseSearchModel searchModel, IPagedList<TObject> objectList, Func<IAsyncEnumerable<TModel>> dataFillFunction)
            where TListModel : BasePagedListModel<TModel>
    {
        if (listModel == null)
            throw new ArgumentNullException(nameof(listModel));

        listModel.Data = await (dataFillFunction?.Invoke()).ToListAsync();
        listModel.Draw = searchModel?.Draw;
        listModel.RecordsTotal = objectList?.TotalCount ?? 0;
        listModel.RecordsFiltered = objectList?.TotalCount ?? 0;

        return listModel;
    }
}
