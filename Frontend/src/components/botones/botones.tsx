import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { Icon } from 'components/Icon';
import React, { CSSProperties, ReactElement } from 'react';
import Estilos from './botones.module.scss';

interface ISubmitButtonProps {
  text: string;
  dataCy?: string;
  loading: boolean;
  className?: string;
}

export function SubmitButton({ text, dataCy = '', className = 'is-primary', loading }: ISubmitButtonProps): ReactElement {
  if (!loading)
    return (
      <button data-cy={dataCy} className={`button ${className}`} type="submit">
        {text}
      </button>
    );
  else
    return (
      <button className={`button ${className} is-loading`} type="button">
        {text}
      </button>
    );
}

interface IBotonProps {
  texto: string;
  dataCy?: string;
  onClick: (e: any) => void;
  value?: string;
  style?: CSSProperties;
  className?: string;
  icono?: IconProp;
  cargando?: boolean;
}

export function Boton({
  texto,
  icono,
  dataCy = '',
  onClick,
  style,
  value,
  className = 'is-primary',
  cargando,
}: IBotonProps): ReactElement {
  return (
    <button
      data-cy={dataCy}
      className={`button ${className} ${cargando ? 'is-loading' : ''}`}
      disabled={cargando}
      type="button"
      style={style}
      onClick={onClick}
      value={value}
    >
      {icono && <Icon faCode={icono} />}
      <span>{texto}</span>
    </button>
  );
}

interface IBotonSalir {
  onClick: () => void;
}

export function BotonSalir({ onClick }: IBotonSalir): ReactElement {
  return <button className={`delete ${Estilos.botonSalir}`} type="button" aria-label="close" onClick={onClick}></button>;
}
