using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Dtos.Languages;

public abstract class LanguageBaseCreateUpdateDto
{
    #region Properties

    public string Name { get; set; }

    public string LanguageCulture { get; set; }

    public bool IsPublished { get; set; }

    public int DisplayNumber { get; set; }

    #endregion
}
