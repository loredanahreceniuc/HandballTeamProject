using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PageDetails
{
    public int TotalItemCount { get; set; }
    public int TotalPageCount { get; set; }
    public int PageSize { get; set; }
}
public class Pagination<TEntity> where TEntity : class
{
    public PageDetails PageDetails { get; set; } = null!;
    public List<TEntity>? Items { get; set; }
}