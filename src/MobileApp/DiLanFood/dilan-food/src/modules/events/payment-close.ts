import { events, EventName } from "zmp-sdk/apis"; // Require: zmp-sdk >= 2.36.0
import { Payment } from "zmp-sdk/apis";

events.on(EventName.PaymentClose, (data) => {
    const resultCode = data?.resultCode;

    console.log("PaymentClose event received with resultCode:", resultCode);

    // kiểm tra resultCode trả về từ sự kiện PaymentClose
    // 0: Đang xử lý
    // 1: Thành công
    // -1: Thất bại

    //Nếu trạng thái đang thực hiện, kiểm tra giao dịch bằng API checkTransaction nếu muốn
    if (resultCode === 0) {
        Payment.checkTransaction({
            data: { zmpOrderId: data?.zmpOrderId },
            success: (rs) => {
                // Kết quả giao dịch khi gọi api thành công
                const { orderId, resultCode, msg, transTime, createdAt } = rs;
            },
            fail: (err) => {
                // Kết quả giao dịch khi gọi api thất bại
                console.log(err);
            },
        });
    } else {
        // Xử lý kết quả thanh toán thành công hoặc thất bại
        const { orderId, resultCode, msg, transTime, createdAt } = data;
    }
});