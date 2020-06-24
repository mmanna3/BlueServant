import React from 'react';
import {ModalForm, ModalContent, ModalFooter} from 'components/Modal';
import { Input, NumericInput, SubmitButton } from "components/Input";
import { crearHabitacion, crearHabitacionSelector } from './slice';
import { useDispatch, useSelector } from 'react-redux'

const Crear = ({esVisible, cerrar}) => {

  const { loading, hasSuccess } = useSelector(crearHabitacionSelector)

  const dispatch = useDispatch();
  const onSubmit = data => {console.log(1);dispatch(crearHabitacion(data));}

  if (hasSuccess)
    cerrar();

  return (
    <ModalForm
        cerrar={() => cerrar()} 
        esVisible={esVisible}
        titulo="Crear habitación"
        onSubmit={onSubmit}
    >      
      <div></div>
      <ModalContent>
        
        <NumericInput label="Camas matrimoniales" name="camasMatrimoniales" />
        <NumericInput label="Camas individuales" name="camasIndividuales" type="number" />
        <NumericInput label="Camas marineras" name="camasMarineras" type="number" />
      </ModalContent>
      <ModalFooter>
        {/* TODO: Crear ModalFooterAceptarCancelar */}
        <div className="container">
          <div className="buttons is-pulled-right">                          
            
            <button type="button" className="button" onClick={() => cerrar()}>Cancelar</button>
            <SubmitButton loading={loading} text="Guardar" />

          </div>
        </div>
      </ModalFooter>
      <div>
        <Input label="Nombre" name="nombre" />
      </div>

    </ModalForm> 
  )
}

export default Crear