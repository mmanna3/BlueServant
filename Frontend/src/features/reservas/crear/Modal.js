import React, {useState} from 'react';
import { ModalForm, Body, Header, FooterAcceptCancel } from 'components/Modal';
import { Input } from "components/Input";
import ValidationSummary from "components/ValidationSummary";
import DateRangePicker from 'components/dateRangePicker/DateRangePicker';
import { crearReserva, cleanErrors, crearReservaSelector } from './slice';
import { useDispatch, useSelector } from 'react-redux';

const Crear = ({isVisible, onHide, onSuccessfulSubmit}) => {  

  const {loading, validationErrors} = useSelector(crearReservaSelector);
  const [resetOnChanged, resetForm] = React.useState(0);
  const [desdeHasta, onDesdeHastaChange] = useState([new Date(), new Date()]);

  const dispatch = useDispatch();
  const onSubmit = data => dispatch(crearReserva(data, onSuccess));  
  
  function onSuccess() {
    onSuccessfulSubmit();
    resetForm(resetOnChanged+1);
  }

  function hide() {
    onHide();
    dispatch(cleanErrors());
  }

  return (
    <ModalForm
        isVisible={isVisible}
        onHide={hide}
        onSubmit={onSubmit}
        resetOnChanged={resetOnChanged}
    >
      <Header title="Alta de reserva" onHide={hide} />
      <Body>
        <ValidationSummary errors={validationErrors} />
        <Input label="Huesped" name="aNombreDe" />
        <DateRangePicker 
          value={desdeHasta}
          onChange={onDesdeHastaChange}
        />
        <Input label="CamasIds" name="CamasIds[0]" />
        
      </Body>
      <FooterAcceptCancel onCancel={hide} loading={loading} />
      
    </ModalForm> 
  )
}

export default Crear;