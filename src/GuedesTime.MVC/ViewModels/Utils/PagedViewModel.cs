namespace GuedesTime.MVC.ViewModels.Utils
{
    public class PagedViewModel<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public string? Search { get; set; }
        public IEnumerable<T> Model { get; set; } = Enumerable.Empty<T>();
    }
}
