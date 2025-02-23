export const APP_ID = window.APP_ID
export const API_URL = import.meta.env.VITE_API_URL
export const SERVER_URL = import.meta.env.VITE_SERVER_URL
export const SECRET_KEY = import.meta.env.VITE_SECRET_KEY
export const PRIVATE_KEY = import.meta.env.VITE_PRIVATE_KEY
export const MERCHANT_ID = import.meta.env.VITE_MERCHANT_ID
export const INIT_URL = new URL(window.location.href)
export const APP_ENV = INIT_URL.searchParams.get('env')
export const IS_DEV = import.meta.env.DEV || APP_ENV === 'DEVELOPMENT'
export const IS_TESTING = import.meta.env.DEV || APP_ENV === 'TESTING'
export const DEFAULT_ORDER_SESSION_ID = INIT_URL.searchParams.get('orderSession') || undefined
