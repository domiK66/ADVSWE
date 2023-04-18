namespace Services.Models.Response;
public class ItemResponse<T>: ResponseModel where T: class {
    public T Data { get; set; }
}
