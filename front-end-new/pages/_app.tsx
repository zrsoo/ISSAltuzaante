import "../styles/globals.css";
import {useEffect, useState} from "react";
import {AuthenticationController} from "../controllers/AuthenticationController";

function MyApp({ Component, pageProps }) {
  return <Component {...pageProps} />;
}

export default MyApp;
