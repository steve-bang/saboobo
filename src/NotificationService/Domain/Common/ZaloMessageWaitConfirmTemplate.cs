
using SaBooBo.Utils;

namespace SaBooBo.Domain.Common;

public class ZaloMessageWaitConfirmTemplate : ZaloMessageTemplate
{
    public ZaloMessageWaitConfirmTemplate(
        string userId,
        Guid orderId,
        double totalPrice,
        string miniZaloAppLink,
        string phoneNumberOwner
    )
    {
        UserZaloId = userId;
        OrderId = orderId;
        TotalPrice = totalPrice;
        MiniZaloAppLink = miniZaloAppLink;
        PhoneNumberOwner = phoneNumberOwner;
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
                                type = "text",
                                align = "left",
                                content = "Đơn hàng của bạn chưa bao gồm phí giao hàng, vui lòng bấm vào tính phí giao hàng để được xác nhận được hàng."
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
                                        style= "grey",
                                        value="Chờ xác nhận"
                                    },
                                    new {
                                        key="Hình thức thanh toán",
                                        value="COD"
                                    },
                                    new {
                                        key="Phí giao hàng",
                                        value="Chưa bao gồm"
                                    },
                                    new {
                                        key="Thành tiền",
                                        value=CurrencyUtil.Format(TotalPrice, CurrencyEnum.VND)
                                    },
                                }
                            }
                    },
                    buttons = new object[] {
                        new {
                            type = "oa.query.show",
                            title = "Yêu cầu tính phí giao hàng",
                            payload = "Tính phí giao hàng"
                        },
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