﻿using Portfolio.Domain.Dtos.Common;
using Portfolio.Domain.Models;
using System.Collections.Generic;

namespace Portfolio.Domain.Dtos.Projects;

public class ProjectDto : BaseDto
{
    #region Properties

    public string Title { get; set; }

    public string Description { get; set; }

    public string ImagePath { get; set; }

    public bool IsPublished { get; set; }

    public string DemoUrl { get; set; }

    public string GithubUrl { get; set; }

    public int DisplayNumber { get; set; }

    public ICollection<SkillDto> Skills { get; set; }

    public IEnumerable<Url> Urls { get; set; }

    public IEnumerable<ProjectPictureDto> Pictures { get; set; }

    #endregion
}
