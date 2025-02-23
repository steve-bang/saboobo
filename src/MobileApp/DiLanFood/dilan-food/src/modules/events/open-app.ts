import { events, EventName } from "zmp-sdk/apis"; // Require: zmp-sdk >= 2.25.3
import { Payment } from "zmp-sdk/apis";

events.on(EventName.OpenApp, (data) => {
  const path = data?.path;

  console.log("OpenApp event received with path:", path);
  // kiểm tra path trả về từ giao dịch thanh toán
  // RedirectPath: đã cung cấp tại trang khai báo phương thức
//   if (path.includes(RedirectPath)) {
//     // Nếu đúng với RedirectPath đã cũng cấp, thực hiện redirect tới path được nhận
//     // Kiểm tra giao dịch bằng API checkTransaction nếu muốn
//     Payment.checkTransaction({
//       data: path,
//       success: (rs) => {
//         // Kết quả giao dịch khi gọi api thành công
//         const { orderId, resultCode, msg, transTime, createdAt } = rs;
//       },
//       fail: (err) => {
//         // Kết quả giao dịch khi gọi api thất bại
//         console.log(err);
//       },
//     });
//   }
});