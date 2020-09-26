import React from 'react';   
import Select from "components/Select";    
import { Icon } from "components/Icon";    

const SelectCama = ({habitaciones, cargando, camasDisponibles, onHabitacionChange}) => {

  return (
    <div className="field field-body is-grouped">

      <div className="field field-body is-grouped">
        <div className="field is-expanded has-addons" style={{minWidth:"200px"}}>
          <span className="control">
            <span className="button is-static">
              Hab.
            </span>
          </span>
          <span className="control is-expanded">
            <span className="control is-expanded">
              <Select name="Habitacion" style={{minWidth:"180px"}} onChange={onHabitacionChange}>
              {cargando ?
              <option>Cargando...</option> :
              habitaciones.map((habitacion, index) => {
                return <option key={habitacion.id} value={index}>{habitacion.nombre} ({habitacion.cantidadDeLugaresLibres})</option>
              })
              }
              </Select>
            </span>
          </span>
        </div>

        <div className="field is-expanded has-addons" style={{minWidth:"280px"}}>
          <span className="control">
            <span className="button is-static">
              Cama
            </span>
          </span>
          <span className="control is-expanded">
            <span className="control is-expanded">
            <Select name="CamasIds[0]" style={{minWidth:"220px"}}>
              {camasDisponibles.length === 0 ?
                <option>No tiene</option> :
                camasDisponibles.map((cama) => {
                  return <option key={cama.id} value={cama.id}>{cama.tipo} - {cama.nombre}</option>
                })
              }
            </Select>
            </span>
          </span>
        </div>
      </div>

      <button className="button has-text-grey has-background-light" type="button" onClick={() => console.log('aas')}>
        <Icon faCode="trash-alt" />
      </button>

    </div>
  )
}

export default SelectCama;