import { combineReducers } from 'redux';

//Para Matías del futuro:
//Hoy opino que todos estos archivos deberían estar en la misma carpeta.
//Pensalo.

import habitacionesReducer from 'store/api/habitacion/listar/slice';
import habitacionesConLugaresLibresReducer from 'store/api/habitacion/listarConLugaresLibres/conLugaresLibresSlice';
import crearHabitacionReducer from 'store/api/habitacion/crear/slice';
import obtenerHabitacionPorIdReducer from 'store/api/habitacion/obtenerPorId/slice';

import huespedesReducer from 'store/api/huespedes/listar/slice';
import crearHuespedReducer from 'store/api/huespedes/crear/slice';

import tablaDeReservas from 'store/app/tablaDeReservas/slice';

import reservasReducer from 'store/api/reserva/listar/slice';
import crearReservaReducer from 'store/api/reserva/crear/slice';
import checkoutsDeHoy from 'store/api/reserva/checkoutsDeHoy/slice';

import loginReducer from 'store/api/usuario/autenticar/slice';

const rootReducer = combineReducers({
  login: loginReducer,

  habitaciones: habitacionesReducer,
  habitacionesConLugaresLibres: habitacionesConLugaresLibresReducer,
  crearHabitacion: crearHabitacionReducer,

  huespedes: huespedesReducer,
  crearHuesped: crearHuespedReducer,

  tablaDeReservas: tablaDeReservas,

  reservas: reservasReducer,
  crearReserva: crearReservaReducer,
  obtenerHabitacionPorId: obtenerHabitacionPorIdReducer,
  checkoutsDeHoy: checkoutsDeHoy,
});

export default rootReducer;
