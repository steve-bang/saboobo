
namespace SaBooBo.NotificationService.Models;


public record ZaloZnsRecipient(
    string UserId
);

public record ZaloZnsMessage(
    ZaloZnsAttachment Attachment
);

public record ZaloZnsAttachment(
    ZaloZnsAttachmentPayload Payload
);

public record ZaloZnsAttachmentPayload(
    string TemplateType,
    string Language,
    ZaloZnsAttachmentPayloadElement[] Elements,
    ZaloZnsAttachmentPayloadButton[] Buttons
);


public record ZaloZnsAttachmentPayloadElement(
    string Type,
    string? ImageUrl,
    string Align,
    object? Content
);

public record ZaloZnsAttachmentPayloadButton(
    string Type,
    string Title,
    object? Payload
);

public class ZaloZnsRequestModel
{
    public ZaloZnsRecipient Recipient { get; set; }
    public ZaloZnsMessage Message { get; set; }

    public ZaloZnsRequestModel(ZaloZnsRecipient recipient, ZaloZnsMessage message)
    {
        Recipient = recipient;
        Message = message;
    }

    // public static ZaloZnsRequestModel BuildWaitConfirm(
    //     string userId,
    //     Guid orderId,
    //     double totalPrice,
    //     string miniZaloAppLink,
    //     string phoneNumberOwner
    // )
    // {
    //     ZaloZnsRecipient recipient = new ZaloZnsRecipient(userId);

    //     ZaloZnsAttachmentPayloadElement[] elements = new ZaloZnsAttachmentPayloadElement[]
    //     {
    //         new ZaloZnsAttachmentPayloadElement(
    //             Type: "text",
    //             Align: "center",
    //             Content: new
    //             {
    //                 Text = "Xác nhận đơn hàng"
    //             }
    //         ),
    //         new ZaloZnsAttachmentPayloadElement(
    //             Type: "text",
    //             Align: "center",
    //             Content: new
    //             {
    //                 Text = $"Mã đơn hàng: {orderId}"
    //             }
    //         ),
    //         new ZaloZnsAttachmentPayloadElement(
    //             Type: "text",
    //             Align: "center",
    //             Content: new
    //             {
    //                 Text = $"Tổng tiền: {totalPrice} VND"
    //             }
    //         ),
    //         new ZaloZnsAttachmentPayloadElement(
    //             Type: "text",
    //             Align: "center",
    //             Content: new
    //             {
    //                 Text = "Nhấn vào nút bên dưới để xác nhận đơn hàng"
    //             }
    //         )
    //     };
    // }
}
