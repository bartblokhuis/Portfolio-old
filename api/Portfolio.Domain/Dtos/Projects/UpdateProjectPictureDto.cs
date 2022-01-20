using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Dtos.Projects;

public class UpdateProjectPictureDto
{
    #region Properties

    public int ProjectId { get; set; }

    public int DisplayNumber { get; set; }

    public int CurrentPictureId { get; set; }

    public int NewPictureId { get; set; }

    #endregion
}

