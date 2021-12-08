using Portfolio.Domain.Dtos.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Domain.Dtos.Projects
{
    public class CreateUpdateProject : BaseDto
    {
        #region Properties

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public bool IsPublished { get; set; }

        public string DemoUrl { get; set; }

        public string GithubUrl { get; set; }

        public int DisplayNumber { get; set; }

        #endregion
    }
}
