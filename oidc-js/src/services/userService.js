import { UserManager } from 'oidc-client';
import { storeUserError, storeUser } from '../actions/authActions'

// const authApiUrl = 'https://localhost:7132'
const authApiUrl = 'https://localhost:7068'

const config = {
  authority: authApiUrl,
  client_id: "oidcMVCAppR",
  redirect_uri: "https://localhost:3000/signin-oidc",
  response_type: "code",
  scope: "openid profile email role weatherApi.read BlobStorage.Api",
  post_logout_redirect_uri: "https://localhost:3000/",
};

const userManager = new UserManager(config)

export async function loadUserFromStorage(store) {
  try {
    let user = await userManager.getUser()
    if (!user) { return store.dispatch(storeUserError()) }
    store.dispatch(storeUser(user))
  } catch (e) {
    console.error(`User not found: ${e}`)
    store.dispatch(storeUserError())
  }
}

export function signinRedirect() {
  return userManager.signinRedirect()
}

export function signinRedirectCallback() {
  return userManager.signinRedirectCallback()
}

export function signoutRedirect() {
  userManager.clearStaleState()
  userManager.removeUser()
  return userManager.signoutRedirect()
}

export function signoutRedirectCallback() {
  userManager.clearStaleState()
  userManager.removeUser()
  return userManager.signoutRedirectCallback()
}

export default userManager