import React from 'react';
import Estilos from './Encabezado.module.scss'

const Encabezado = ({habitaciones, mostrarDetalle}) => {

  var iconosCamas = {
    Individual: 'individual',
    Matrimimonial: 'matrimonial',
    'Cucheta Abajo': 'cucheta-abajo',
    'Cucheta Arriba': 'cucheta-arriba',    
  }

  var estilosIconoCama = {
    Individual: 'iconoCamaIndividual',
    Matrimimonial: 'iconoCamaMatrimonial',
    'Cucheta Abajo': 'iconoCamaCucheta',
    'Cucheta Arriba': 'iconoCamaCucheta',    
  }

  return (
        <thead className="is-bordered">
          <tr>
            <th rowSpan="2" className={Estilos.interseccion}></th>
            {habitaciones.map((habitacion, i) => 
              <th key={i} className={`has-text-weight-medium has-text-centered ${Estilos.habitacionContainer}`} colSpan={habitacion.camas.length}>
                <div className={Estilos.habitacion} onClick={() => mostrarDetalle(habitacion.id)}>
                  {habitacion.nombre} {habitacion.esPrivada ? '\uf023' : ''}
                </div>                
              </th>
            )}
          </tr>        
          <tr>
            {habitaciones.map((habitacion) => 
              habitacion.camas.map((cama,i) =>
                <th key={i} className={`has-text-weight-medium ${Estilos.cama}`}>                  
                  <img className={Estilos[`${estilosIconoCama[cama.tipo]}`]} src={`images/icons/camas/${iconosCamas[cama.tipo]}.svg`} alt={cama.tipo}/>
                </th>
              )
            )}
          </tr>
        </thead>
  )
}

export default Encabezado;