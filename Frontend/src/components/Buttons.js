import React from "react";

export function SubmitButton({ text, loading }) {
  if (!loading)
    return <button className="button is-primary" type="submit">{text}</button>
  else
    return <button className="button is-primary is-loading" type="button">{text}</button>
}

export function Button({ text, ...otrosAtributos }) {
  return <button className="button is-primary has-text-weight-medium" type="button" {...otrosAtributos}>{text}</button>
}