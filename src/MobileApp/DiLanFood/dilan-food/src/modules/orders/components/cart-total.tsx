import React, { useRef } from 'react'
import { useResizeObserver } from 'usehooks-ts'
import { Button, Text, useNavigate, useSnackbar } from 'zmp-ui'
import { events, EventName } from "zmp-sdk/apis";

import { formatMoney } from '@/utils/format'

import { useCart } from '../use-cart'
import { useOrderSessionActions } from '../use-order-session'
import { useSubmitOrder } from '../use-orders'
import { createOrder } from '@/libs/zalo.action'
import { PAYMENT_METHODS } from '@/constants/paymentMethod'
import { Payment } from 'zmp-sdk'
import { CheckoutSDK } from 'zmp-sdk/apis'

export function CartTotal() {
  const snackbar = useSnackbar()
  const { items, total, actions } = useCart()
  const ref = useRef<HTMLDivElement>(null)
  const { height } = useResizeObserver({ ref, box: 'border-box' })
  const navigate = useNavigate()

  const orderMutation = useSubmitOrder()
  const orderSessionActions = useOrderSessionActions()

  async function handleCheckoutCart() {

    Payment.selectPaymentMethod({
      channels: [
        { method: PAYMENT_METHODS.COD_SANDBOX },
        { method: PAYMENT_METHODS.BANK_SANDBOX, subMethod: 'ACB' },
      ],
      success: async (data: any) => {
        // Lựa chọn phương thức thành công
        const { method, isCustom, logo, displayName, subMethod } = data;

        const orderId = await createOrder(
          {
            desc: "Test order",
            amount: total,
            item: Object.values(items).map((item) => ({
              id: item.product.id,
              amount: (item.product.price * item.quantity).toString(),
            })),
            extradata: JSON.stringify({
              notes: "This is a test order"
            }),
            method: JSON.stringify({
              id: method,
              isCustom: false,
              subMethod: subMethod
            }),
          },
          (error: any) => {
            console.log("Error creating order: ", error);
          }
        )

        console.log("Order created: ", orderId);

      },
      fail: (err: any) => {
        // Tắt trang lựa chọn phương thức hoặc xảy ra lỗi
        console.log(err);
      },
    });




    // handle checkout cart     
    // orderMutation.mutate(
    //   {
    //     items: Object.values(items).map((item) => ({
    //       productId: item.product.id,
    //       quantity: item.quantity,
    //       note: item.note || '',
    //       toppingIds: item.toppings?.map((t) => t.id) || [],
    //     })),
    //   },
    //   {
    //     onSuccess: (data) => {
    //       orderSessionActions.setId(data.orderSessionId)
    //       actions.clear()
    //       snackbar.openSnackbar({
    //         type: 'success',
    //         text: 'Gọi món thành công',
    //       })
    //       navigate(Routes.merchant.orders(), { animate: false, replace: true })
    //     },
    //     onError() {
    //       snackbar.openSnackbar({
    //         type: 'error',
    //         text: 'Đã xảy ra lỗi, không thể thêm món',
    //       })
    //     },
    //   },
    // )
  }

  // Evnet PaymentClose
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

          console.log("PaymentClose event received with orderId:", orderId);
          console.log("PaymentClose event received with msg:", msg);
          console.log("PaymentClose event received with transTime:", transTime);
          console.log("PaymentClose event received with createdAt:", createdAt);
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

  // Event OpenApp
  events.on(EventName.OpenApp, (data) => {
    const path = data?.path;

    console.log("OpenApp event received with path:", path);
    // kiểm tra path trả về từ giao dịch thanh toán
    // RedirectPath: đã cung cấp tại trang khai báo phương thức
    //if (path.includes(RedirectPath)) {
    // Nếu đúng với RedirectPath đã cũng cấp, thực hiện redirect tới path được nhận
    // Kiểm tra giao dịch bằng API checkTransaction nếu muốn
    Payment.checkTransaction({
      data: path,
      success: (rs) => {
        // Kết quả giao dịch khi gọi api thành công
        const { orderId, resultCode, msg, transTime, createdAt } = rs;

        console.log("OpenApp event received with resultCode:", resultCode);
        console.log("OpenApp event received with orderId:", orderId);
        console.log("OpenApp event received with msg:", msg);
        console.log("OpenApp event received with transTime:", transTime);
        console.log("OpenApp event received with createdAt:", createdAt);
      },
      fail: (err) => {
        // Kết quả giao dịch khi gọi api thất bại
        console.log(err);
      },
    });
    //}
  });

  if (Object.keys(items).length === 0) return null

  return (
    <>
      {/* <div style={{ height: (height || 0) + 16 }} /> */}
      <div className="fixed bottom-0 left-0 right-0 with-bottom-nav bg-background shadow-[0px_-2px_6px_0px_#0D0D0D24]">
        <div className="p-4">
          <div className="flex gap-2 justify-between mb-2">
            <Text size="large" className="font-medium">
              Tổng cộng
            </Text>
            <Text size="xLarge" className="text-primary font-medium">
              {formatMoney(total)}
            </Text>
          </div>
          <div className="mt-4" />
          <Button fullWidth size="large" onClick={handleCheckoutCart} loading={orderMutation.isPending}>
            Gọi món
          </Button>
        </div>
      </div>
    </>
  )
}
