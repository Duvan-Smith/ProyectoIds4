import { createUserManager } from "redux-oidc";

const uriBase = `${window.location.protocol}//${window.location.hostname}${
  window.location.port ? `:${window.location.port}` : ""
}`;

const settings = {
  client_id: "oidcMVCApp",
  client_secret: "ProCodeGuide",
  redirect_uri: `${uriBase}/callback`,
  post_logout_redirect_uri: `${uriBase}/signout-oidc-callback`,
  response_type: "code",
  scope: "weatherApi.read",
  authority: "https://localhost:7132/",
  silent_redirect_uri: `${uriBase}/silent_renew.html`,
  automaticSilentRenew: true,
  filterProtocolClaims: true,
  loadUserInfo: true,
  monitorSession: false,
  // response_mode: "query",
  // webAuthResponseType: "code",
};
const userManager = createUserManager(settings);

export default userManager;
