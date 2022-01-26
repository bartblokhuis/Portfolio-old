using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Domain.Dtos.Projects;

public class CreateUpdateProjectDto
{
    #region Properties

    [Required]
    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsPublished { get; set; }

    public int DisplayNumber { get; set; }

    #endregion
}
