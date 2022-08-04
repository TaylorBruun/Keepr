import { createRouter, createWebHashHistory } from 'vue-router'
import { authGuard, authSettled } from '@bcwdev/auth0provider-client'
import { AppState } from './AppState'

function loadPage(page) {
  return () => import(`./pages/${page}.vue`)
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: loadPage('HomePage')
  },
 
  {
    path: '/account',
    name: 'Account',
    component: loadPage('AccountPage'),
    beforeEnter: authGuard
  },
  {
    path: '/profile/:id',
    name: 'Profile',
    component: loadPage('ProfilePage'),
    beforeEnter: authSettled
  },
  {
    path: '/vault/:id',
    name: 'Vault',
    component: loadPage('VaultPage'),
    beforeEnter: authSettled
  }
]

export const router = createRouter({
  linkActiveClass: 'router-link-active',
  linkExactActiveClass: 'router-link-exact-active',
  history: createWebHashHistory(),
  routes
})
