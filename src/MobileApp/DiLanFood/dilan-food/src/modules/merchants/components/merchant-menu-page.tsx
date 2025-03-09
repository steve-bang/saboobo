import React, { Suspense, useEffect, useId } from 'react'
import { Box, Header, Page, Text } from 'zmp-ui'

import { ChipListRef } from '@/components/chip-list'
import { MerchantNotFoundError } from '@/constants/errors'
import { CartFloatingCounter } from '@/modules/orders/components/cart-floating-counter'
import { ProductItem } from '@/modules/products/components'
import { useMenu } from '@/modules/products/use-menu'

import { useMerchant } from '../use-merchant'
import { MenuEmpty } from './menu-empty'
import { MerchantMenuPageLoading } from './merchant-menu-page-loading'
import { Banner } from './banner'
import ModalSpinner from '@/components/modal-spinner'
import { useUserStore } from '@/state/user-state'
import ModalSignInZalo from './modal-sign-in-zalo'

const ChipList = React.lazy(() => import('@/components/chip-list'))

export function MerchantMenuPage() {
  const { data: merchant, isLoading: merchantLoading } = useMerchant();
  const { data: menu, isLoading: menuLoading } = useMenu();

  const id = useId()

  const isLoading = merchantLoading || menuLoading

  const chipListRef = React.useRef<ChipListRef<React.Key>>(null)
  const scrollingRef = React.useRef<boolean>(false)
  const scrollingTimeout = React.useRef<ReturnType<typeof setTimeout>>()

  const isAuthenticated = useUserStore((state) => state.isAuthenticated)

  const [modalVisible, setModalVisible] = React.useState(false)

  useEffect(() => {
    function onScroll() {
      if (scrollingRef.current) return
      const items = document.querySelectorAll(`.section-wrapper-${CSS.escape(id)}`)
      let middleId = ''
      const middle = window.innerHeight / 2
      items.forEach((item) => {
        const rect = item.getBoundingClientRect()
        if (rect.top < middle && rect.bottom > middle) {
          middleId = item.getAttribute('data-id') || ''
        }
      })
      if (middleId) {
        chipListRef.current?.setActive(Number(middleId))
      }
    }
    const page = document.querySelector('.zaui-page')
    page?.addEventListener('scroll', onScroll)
    return () => {
      page?.removeEventListener('scroll', onScroll)
    }
  }, [id])

  if (isLoading) return <MerchantMenuPageLoading />
  if (!merchant || !menu) throw new MerchantNotFoundError()

  const options = menu.map((item) => ({ label: item.category.name, value: item.category.id }))

  return (
    <Page>
      <Header
        className="no-divider"
        showBackIcon={false}
        title={
          (
            <Box flex alignItems="center" className="space-x-2">
              {
                merchant.logoUrl && (
                  <img
                    className="w-8 h-8 rounded-lg border-inset"
                    src={merchant.logoUrl}
                  />
                )
              }
              <Box>
                <Text.Title size="small">{merchant.name}</Text.Title>
              </Box>
            </Box>
          ) as unknown as string
        }
      />

      {/* Chip List */}

      <div className="bg-background fixed left-0 right-0 top-[calc(var(--zaui-safe-area-inset-top)+44px)] z-10 shadow-[0px_0px_0px_1px] shadow-divider overflow-hidden">
        <Suspense fallback={<div className="h-[48px]" />}>
          <ChipList
            ref={chipListRef}
            className="py-2 bg-background"
            defaultValue={options[0]}
            options={options}
            onChange={(option) => {
              const page = document.querySelector('.zaui-page')
              const el = document.getElementById(`section-${id}-${option.value}`)
              scrollingRef.current = true
              clearTimeout(scrollingTimeout.current)
              if (page && el) {
                page.scrollTo({
                  top: el.offsetTop - 152,
                  behavior: 'smooth',
                })
              }
              scrollingTimeout.current = setTimeout(() => {
                scrollingRef.current = false
              }, 1500)
            }}
          />
        </Suspense>
      </div>
      <div style={{ height: 90, marginBottom: 20 }} />

      {/* Banner */}
      <Banner />


      {/* Region Menu */}
      {menu.length === 0 && <MenuEmpty />}
      {menu.length > 0 && (
        <div className="bg-background">
          <div className="px-4 flex flex-col gap-4">
            {menu.map((menuItem) => (
              <div
                key={menuItem.category.id}
                data-id={menuItem.category.id}
                className={`section-wrapper-${id}`}
                id={`section-wrapper-${id}-${menuItem.category.id}`}
              >
                <Text size="large" className="font-medium" id={`section-${id}-${menuItem.category.id}`}>
                  {menuItem.category.name}
                </Text>
                <div className="mt-3 grid grid-cols-2 gap-3">
                  {menuItem.products?.map((product) => <ProductItem key={product.id} item={product} setShowModalVisible={setModalVisible} />)}
                </div>
              </div>
            ))}
          </div>
        </div>
      )}
      <CartFloatingCounter />
      <div className="bg-white" style={{ height: 48 }} />
      <ModalSpinner visible={modalVisible} onClose={() => setModalVisible(false)} />

      <ModalSignInZalo modalVisible={!isAuthenticated} setModalVisible={setModalVisible} />
    </Page>
  )
}
