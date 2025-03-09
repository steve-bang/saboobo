import React, { useEffect, useRef } from 'react'
import { Page, useNavigate } from 'zmp-ui'

import { MerchantNotFoundError } from '@/constants/errors'
import { Routes } from '@/constants/routes'
import { useMerchant, useMerchantEnableOrder } from '@/modules/merchants/use-merchant'

import { useOrdersTab } from '../use-orders-tab'
import { CartList } from './cart-list'
import { CartTotal } from './cart-total'
import { OrdersHeader } from './orders-header'
import { Delivery } from './delivery'
import { Divider } from '@/components/divider'

import { useCart } from '../use-cart'
import { useUserStore } from '@/state/user-state'
import ModalSpinner from '@/components/modal-spinner'
import RequiredAuth from '@/components/required-auth'
import ModalSignInZalo from '@/modules/merchants/components/modal-sign-in-zalo'

export function OrdersPage() {

  const { isAuthenticated, accessToken } = useUserStore((state) => state);

  const navigate = useNavigate()
  const [tab] = useOrdersTab()
  const { data, isLoading } = useMerchant()
  const ref = useRef<HTMLDivElement>(null)
  const enableOrder = useMerchantEnableOrder();
  const { actions } = useCart()

  const [modalVisible, setModalVisible] = React.useState(false)

  useEffect(() => {
    if (data && !enableOrder) {
      navigate(Routes.merchant.page(), { replace: true, animate: false })
    }

    // Loading cart from server
    if (accessToken) {
      setModalVisible(true)
      actions.init(accessToken);
      setModalVisible(false)
    }

  }, [navigate, data, enableOrder])

  if (isLoading) return null
  if (!data) throw new MerchantNotFoundError()


  return (
    <Page className="my-10 space-y-3 px-4">
      <OrdersHeader />

      {/* <div style={{ height: 118 }} /> */}
      {/* {tab === 'cart' && (
        <div className='overflow-x-auto'>
          <CartList />
          <Delivery />
          <Divider size={32} className="flex-1" />
          <CartTotal />
        </div>
      )} */}
      {/* {tab === 'orders' && (
        <>
          <div className="mt-2" />
          <OrdersList />
          <div className="mt-4" />
          <OrdersTotal />
          <div className="mt-4" />
        </>
      )} */}

      {
        <div className='overflow-x-auto'>
          <CartList />
          <Delivery />
          <Divider size={32} className="flex-1" />
          <CartTotal />
        </div>
      }

      <ModalSpinner visible={modalVisible} onClose={() => setModalVisible(false)} />

      <ModalSignInZalo modalVisible={!isAuthenticated} setModalVisible={setModalVisible} />
    </Page >
  )
}
