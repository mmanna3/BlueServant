import { combineReducers } from 'redux';
import api from 'store/api/api';
import tablaDeReservas from 'store/app/tablaDeReservas/slice';

const rootReducer = combineReducers({
  autenticarUsuario: api.usuarios.autenticar.reducer,

  habitaciones: api.habitaciones.listar.reducer,
  obtenerHabitacionPorId: api.habitaciones.obtenerPorId.reducer,
  habitacionesConLugaresLibres: api.habitaciones.listarConLugaresLibres.reducer,
  crearHabitacion: api.habitaciones.crear.reducer,
  habilitarHabitacion: api.habitaciones.habilitar.reducer,
  deshabilitarHabitacion: api.habitaciones.deshabilitar.reducer,
  habilitarCama: api.habitaciones.habilitarCama.reducer,
  deshabilitarCama: api.habitaciones.deshabilitarCama.reducer,

  pasajeros: api.pasajeros.listar.reducer,
  crearPasajero: api.pasajeros.crear.reducer,
  obtenerPasajeroPorDniOPasaporte: api.pasajeros.obtenerPorDniOPasaporte.reducer,

  tablaDeReservas: tablaDeReservas,

  obtenerReservaPorId: api.reservas.obtenerPorId.reducer,
  reservas: api.reservas.listar.reducer,
  reservasVigentes: api.reservas.listarVigentes.reducer,
  crearReserva: api.reservas.crear.reducer,
  checkoutsDeHoy: api.reservas.checkoutsDeHoy.reducer,
  cancelar: api.reservas.cancelar.reducer,
  hacerCheckIn: api.reservas.hacerCheckIn.reducer,
  hacerCheckOut: api.reservas.hacerCheckOut.reducer,
  cantidadDeCheckInsYCheckOutsDeHoy: api.reservas.cantidadDeCheckInsYCheckOutsDeHoy.reducer,
});

export default rootReducer;
