import React, { useState } from 'react'
import { signoutRedirect } from '../services/userService'
import { useSelector } from 'react-redux'
import { prettifyJson } from '../utils/jsonUtils'
function Home() {
  const user = useSelector(state => state.auth.user)
  function signOut() {
    signoutRedirect()
  }

  return (
    <div>
      <p>Hello, <b>{user.profile.given_name}</b>.</p>
      <p>You have been signed in successfully!</p>
      <pre>
        <code>{prettifyJson(user)}</code>
      </pre>
      <button className="button button-clear" onClick={() => signOut()}>Sign Out</button>
    </div>
  )
}

export default Home
