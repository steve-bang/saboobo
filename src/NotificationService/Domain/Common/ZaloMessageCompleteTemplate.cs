
using SaBooBo.Utils;

namespace SaBooBo.Domain.Common;

public class ZaloMessageCompleteTemplate : ZaloMessageTemplate
{
    public double ShippingTotal { get; set; }

    public ZaloMessageCompleteTemplate(
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
                                content = "Đơn hàng của bạn đã được giao thành công. Cảm ơn bạn đã sử dụng dịch vụ.",
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
                                        style= "green",
                                        value="Đã hoàn thành"
                                    },
                                    new {
                                        key="Hình thức thanh toán",
                                        value="COD"
                                    },
                                    new {
                                        key="Phí giao hàng",
                                        value=CurrencyUtil.Format(ShippingTotal, CurrencyEnum.VND)
                                    },
                                    new {
                                        key="Thành tiền",
                                        value=CurrencyUtil.Format(TotalPrice, CurrencyEnum.VND)
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