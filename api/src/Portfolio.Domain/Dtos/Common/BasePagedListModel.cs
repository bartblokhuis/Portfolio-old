using System.Collections.Generic;

namespace Portfolio.Domain.Dtos.Common;

public record BasePagedListModel<T>
{
    public IEnumerable<T> Data { get; set; }

    public string Draw { get; set; }

    public int RecordsFiltered { get; set; }

    public int RecordsTotal { get; set; }
}
