import React from 'react';
import Cell from './Cell/Cell.js'
import Styles from './Table.module.scss'

const Table = () => {  

  const [selectionData, setSelected] = React.useState({hasStarted: false, currentColumn: -1, currentSelection: []});
  
  const selectFirstRow = (columnId, rowId) => {
    updateSelectionData(true, columnId, rowId);
  }

  const endSelection = () => {
    updateSelectionData(false, -1);
  }

  const canBeSelected = (columnId, rowId) => {
    
    if (selectionData.currentColumn === columnId && isContiguous(rowId) && selectionData.hasStarted) {
      updateSelectionData(true, columnId, rowId);
      return true;
    }

    return false;
  }

  const canBeClickedForEndingSelection = (columnId, rowId) => {
    return selectionData.currentColumn === columnId && isLastRow(rowId) && selectionData.hasStarted;
  }

  const isContiguous = (rowId) => selectionData.currentSelection[selectionData.currentSelection.length - 1] + 1 === rowId;

  const isLastRow = (rowId) => selectionData.currentSelection[selectionData.currentSelection.length - 1] === rowId;

  const updateSelectionData = (hasStarted, currentColumn, newSelectedRow) => {
    var copy = selectionData;
    selectionData.hasStarted = hasStarted;
    selectionData.currentColumn = currentColumn;
    
    if (newSelectedRow)
      selectionData.currentSelection.push(newSelectedRow);    
  
    setSelected(copy);
  }

  var head = [
    {
      nombre: 'Azul',
      camas: ['Individual: A', 'Individual: B']
    },
    {
      nombre: 'Roja',
      camas: ['Matrimonial: 1', 'Mar. Arriba: 2', 'Mar. Abajo: 3']
    },
    {
      nombre: 'Verde',
      camas: ['Matrimonial: Matri', 'Individual: Indi']
    }
  ];

  return (  
      <table className={`table is-hoverable is-bordered is-fullwidth ${Styles.table}`}>
        <thead className="is-bordered">
          <tr>
            <th rowSpan="2"></th>
            {head.map((habitacion) => 
              <th colSpan={habitacion.camas.length}>Habitación {habitacion.nombre}</th>
            )}
          </tr>        
          <tr>
            {head.map((habitacion) => 
              habitacion.camas.map((cama) =>
                <th>{cama}</th>
              )
            )}
          </tr>
        </thead>
        <tbody>
          {[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15].map((e, i) => 
              <tr key={i}>
                <td>{e}/07</td>
                {[0,1,2,3,4,5,6].map((e, column) =>
                    <Cell
                      startSelection={() => selectFirstRow(column, i)}
                      endSelection={() => endSelection()} 
                      selectionData={selectionData} 
                      canBeSelected={() => canBeSelected(column, i)}
                      canBeClickedForEndingSelection={() => canBeClickedForEndingSelection(column, i)}
                    />                    
                )}
              </tr>              
          )}
        </tbody>
    </table>
  )
}

export default Table;