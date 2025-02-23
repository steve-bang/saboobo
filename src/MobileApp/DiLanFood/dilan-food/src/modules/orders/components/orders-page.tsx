import React, { useEffect, useRef } from 'react'
import { Page, useNavigate } from 'zmp-ui'

import { MerchantNotFoundError } from '@/constants/errors'
import { Routes } from '@/constants/routes'
import { useMerchant, useMerchantEnableOrder } from '@/modules/merchants/use-merchant'

import { useOrdersTab } from '../use-orders-tab'
import { CartList } from './cart-list'
import { CartTotal } from './cart-total'
import { OrdersHeader } from './orders-header'
import { OrdersList } from './orders-list'
import { OrdersTabs } from './orders-tabs'
import { OrdersTotal } from './orders-total'
import { Delivery } from './delivery'
import { Divider } from '@/components/divider'

export function OrdersPage() {
  const navigate = useNavigate()
  const [tab] = useOrdersTab()
  const { data, isLoading } = useMerchant()
  const ref = useRef<HTMLDivElement>(null)
  const enableOrder = useMerchantEnableOrder()

  useEffect(() => {
    if (data && !enableOrder) {
      navigate(Routes.merchant.page(), { replace: true, animate: false })
    }
  }, [navigate, data, enableOrder])

  if (isLoading) return null
  if (!data) throw new MerchantNotFoundError()

  return (
    <Page>
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

      <div className='overflow-x-auto'>
        <CartList />
        <Delivery />
        <Divider size={32} className="flex-1" />
        <CartTotal />
      </div>
    </Page>
  )
}
