using System;

namespace Portfolio.Domain.Dtos.Blogs;

public  class UpdateBlogDto : BaseBlogDto
{
    #region Properties

    public int Id { get; set; }

    public string Content { get; set; }

    #endregion
}

