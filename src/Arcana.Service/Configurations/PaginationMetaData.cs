namespace Arcana.Service.Configurations;

public class PaginationMetaData(int totalCount, PaginationParams @params)
{
    public int CurrentPage { get; set; } = @params.Index;
    public int TotalPages { get; set; } = Convert.ToInt32(Math.Ceiling(totalCount / (decimal)@params.PageSize));
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
}
