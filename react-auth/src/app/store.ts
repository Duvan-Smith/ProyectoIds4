import { configureStore, ThunkAction, Action } from "@reduxjs/toolkit";
import createOidcMiddleware, { loadUser, reducer } from "redux-oidc";
import counterReducer from "../features/counter/counterSlice";
import userManager from "../views/Auth/Utils/userManager";

const oidcMiddleware = createOidcMiddleware(userManager);

export const store = configureStore({
  reducer: {
    oidc: reducer,
    counter: counterReducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: false,
    }).concat([oidcMiddleware]),
  devTools: true,
});

loadUser(store, userManager);

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
