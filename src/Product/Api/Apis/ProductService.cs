public class ProductServices(
    IMediator mediator,
    ILogger<ProductServices> logger)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<ProductServices> Logger { get; } = logger;
}
