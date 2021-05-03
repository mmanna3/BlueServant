import Drp from '@wojtekmaj/react-daterange-picker';
import { Input } from 'components/Input';
import React from 'react';
import { useForm } from 'react-hook-form';
import { convertirAString } from 'utils/Fecha';
import Styles from './DateRangePicker.module.scss';

const DateRangePicker = ({ valor, etiqueta, actualizarValor, desdeName, hastaName }) => {
  const { setValue } = useForm();

  return (
    <div className="field">
      <label className="label">{etiqueta ? etiqueta : 'Desde - Hasta'}</label>
      <div className="control">
        <Drp
          onChange={valor => {
            actualizarValor(valor);
            setValue(desdeName, convertirAString(valor[0]));
            setValue(hastaName, convertirAString(valor[1]));
          }}
          value={valor}
          className={Styles.dateRangePicker}
          calendarClassName={Styles.calendar}
          clearAriaLabel="Reiniciar"
          minDate={new Date()}
        />
      </div>
      <Input style={{ display: 'none' }} name={desdeName} defaultValue={convertirAString(valor[0])} />
      <Input style={{ display: 'none' }} name={hastaName} defaultValue={convertirAString(valor[1])} />
    </div>
  );
};

export default DateRangePicker;
