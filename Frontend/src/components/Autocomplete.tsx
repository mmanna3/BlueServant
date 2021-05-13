import { CSSObject } from '@emotion/serialize';
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import React, { ReactElement, useEffect, useState } from 'react';
import { Controller, useFormContext } from 'react-hook-form';
import Select, { components, ControlProps } from 'react-select';
import { Icon } from './Icon';

interface IProps {
  label?: string;
  opciones: ILabelValue[];
  opcionInicial: ILabelValue;
  name: string;
  placeholder?: string;
  register?: () => any;
  icono?: IconProp;
}

export interface ILabelValue {
  value: string;
  label: string;
}

export const Autocomplete = ({ label, opciones, opcionInicial, name, placeholder = '', icono }: IProps): ReactElement => {
  const [valor, actualizarValor] = useState<ILabelValue>(opcionInicial);
  const { setValue } = useFormContext();

  useEffect((): void => {
    setTimeout((): void => {
      setValue(name, valor.value);
    }, 100);
  }, [opcionInicial, name, valor, setValue]);

  const ValueContainer = ({ children, ...props }: any): any => {
    return (
      components.ValueContainer && (
        <components.ValueContainer {...props}>
          {!!children && icono && <Icon faCode={icono} style={{ position: 'absolute', left: 8 }} />}
          {children}
        </components.ValueContainer>
      )
    );
  };

  // Tuneo para que quede igual a los Bulma Inputs
  const styles = {
    valueContainer: (base: CSSObject): CSSObject =>
      icono
        ? {
            ...base,
            paddingLeft: 38,
          }
        : base,
    control: (base: CSSObject, state: ControlProps<any, any, any>): CSSObject => ({
      ...base,
      boxShadow: 'inset 0 0.0625em 0.125em rgb(10 10 10 / 5%) !important',
      borderColor: '#dbdbdb',
      // Si juego con el state.isFocused acá, no me limpia el estilo cuando pierde el foco
      // El menuIsOpen tampoco parece andar muy bien
      '&:hover': {
        borderColor: state.isFocused ? '#3260a8' : 'hsl(0, 0%, 71%)',
        color: state.isFocused ? '#3260a8' : '#222',
        // boxShadow: '0 0 0 0.125em rgba(hsl(217, 71%,  53%), 0.25)',  //Debería ser el mismo que en los inputs de Bulma, pero no anda
      },
    }),
    dropdownIndicator: (base: CSSObject): CSSObject => ({
      ...base,
      color: 'inherit',
    }),
  };

  return (
    <Controller
      name={name}
      defaultValue={opcionInicial}
      render={({ field }): ReactElement => (
        <>
          {label && <label className="label">{label}</label>}
          <Select
            options={opciones}
            {...field}
            value={opciones.find((c): boolean => c.value === field.value)}
            onChange={(val): void => {
              field.onChange(val?.value);
              actualizarValor(val);
            }}
            placeholder={placeholder}
            defaultValue={field.value}
            components={{ ValueContainer }}
            styles={styles}
          />
        </>
      )}
    />
  );
};