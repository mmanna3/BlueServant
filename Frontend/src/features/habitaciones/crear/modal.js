import React from 'react';
import Modal from 'components/Modal';
import { useForm } from "react-hook-form";

const Crear = ({esVisible, cerrar}) => {

  const { register, handleSubmit, watch, errors } = useForm();
  const onSubmit = data => console.log(data);

  console.log(watch("example")); // watch input value by passing the name of it

  return (
    <Modal
        cerrar={() => cerrar()} 
        esVisible={esVisible}
        titulo="Crear habitación"
    >
      
      {/* "handleSubmit" will validate your inputs before invoking "onSubmit" */}
      <form onSubmit={handleSubmit(onSubmit)}>
      {/* register your input into the hook by invoking the "register" function */}
        {/* <input name="nombre" defaultValue="test" ref={register} /> INTERESANTE LO DE DEFAULTVALUE*/}  
        <input name="nombre" defaultValue="test" ref={register} />
        
        {/* include validation with required or other standard HTML validation rules */}
        <input name="exampleRequired" ref={register({ required: true })} />
        {/* errors will return when field validation fails  */}
        {errors.exampleRequired && <span>This field is required</span>}
        
        <input type="submit" />
      </form>
      
    </Modal> 
  )
}

export default Crear