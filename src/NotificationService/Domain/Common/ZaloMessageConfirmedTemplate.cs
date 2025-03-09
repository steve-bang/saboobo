
namespace SaBooBo.Domain.Common;

public class ZaloMessageConfirmedTemplate : ZaloMessageTemplate
{

    public double ShippingTotal { get; set; }

    public ZaloMessageConfirmedTemplate(
        string userId,
        Guid orderId,
        double totalPrice,
        string miniZaloAppLink,
        string phoneNumberOwner,
        double shippingTotal
    )
    {
        UserZaloId = userId;
        OrderId = orderId;
        TotalPrice = totalPrice;
        MiniZaloAppLink = miniZaloAppLink;
        PhoneNumberOwner = phoneNumberOwner;
        ShippingTotal = shippingTotal;
    }

    public override object BuildMessage() => new
    {
        recipient = new
        {
            user_id = UserZaloId
        },
        message = new
        {
            attachment = new
            {
                type = "template",
                payload = new
                {
                    template_type = "transaction_order",
                    language = "VI",
                    elements = new object[] {
                            new {
                                image_url = "https://sabboboac.blob.core.windows.net/media-service/Order_banner_01_optimized_1000.jpg",
                                type = "banner",
                            },
                            new {
                                type = "header",
                                content = "Cảm ơn bạn đã mua hàng tại cửa hàng.",
                                align = "left",
                            },
                            new {
                                type = "table",
                                content = new object[]{
                                    new {
                                        key="Mã đơn hàng",
                                        value=OrderId.ToString()
                                    },
                                    new {
                                        key="Trạng thái",
                                        style= "blue",
                                        value="Xác nhận"
                                    },
                                    new {
                                        key="Hình thức thanh toán",
                                        value="COD"
                                    },
                                    new {
                                        key="Phí giao hàng",
                                        value=ShippingTotal.ToString("C0")
                                    },
                                    new {
                                        key="Thành tiền",
                                        value=TotalPrice.ToString("C0")
                                    }
                                }
                            }
                    },
                    buttons = new object[] {
                        new {
                            type = "oa.open.url",
                            title = "Xem chi tiết",
                            payload = new {
                                url = MiniZaloAppLink
                            }
                        },
                        new {
                            type = "oa.open.phone",
                            title = "Liên hệ cửa hàng",
                            payload = new {
                                phone_code = PhoneNumberOwner
                            }
                        }
                }

                }
            }
        }
    };
}