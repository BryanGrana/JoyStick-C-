# Joystick Virtual en Windows Forms

Este proyecto es una aplicación de escritorio desarrollada en C# que simula un joystick virtual utilizando la biblioteca `System.Windows.Forms`. Permite interactuar con el teclado simulando las teclas WASD o las teclas de flechas mediante el movimiento del joystick.

## Características

- Simula un joystick virtual con un panel circular.
- Compatible con dos modos de control: teclas WASD o teclas de flechas.
- Detecta movimientos del ratón para desplazar el joystick dentro de un radio máximo.
- Regresa a la posición inicial al hacer clic derecho.
- Envia comandos de teclas según la dirección del movimiento.

## Requisitos del sistema

- Windows 10 o superior.
- .NET Framework 4.8 o superior.

## Instalación

1. Clona este repositorio:

   ```bash
   git clone https://github.com/tu-usuario/joystick-virtual.git
   ```
2. Abre el archivo del proyecto en Visual Studio.

3. Compila y ejecuta el proyecto.

Uso

1. Ejecuta la aplicación. Se abrirá una ventana con un joystick circular en el centro.

2. Usa el ratón para mover el joystick dentro de un radio limitado. El joystick enviará comandos de teclas según la dirección del movimiento:

3. WASD para los modos de teclado por defecto.

  - Flechas si cambias el modo de control.

  - Haz clic derecho para resetear el joystick a su posición inicial.

4. Usa el botón “Alternar Modo (WASD/Flechas)” para cambiar entre modos de control.

## Configuración

Puedes ajustar los siguientes parámetros en el código:

- maxDistance: Define el radio máximo de movimiento del joystick.

- usarWASD: Define el modo de control inicial (WASD o Flechas).


Contribución

1. Haz un fork del repositorio.

2. Crea una rama para tus cambios:

  ```bash
  git checkout -b feature/nueva-funcionalidad
  ```
3. Realiza tus cambios y haz un commit:

  ```bash
  git commit -m "Añadida nueva funcionalidad"
  ```
4. Envía tus cambios:

  ```bash
  git push origin feature/nueva-funcionalidad
  ```
5. Abre un Pull Request.

## Autor

Desarrollado por Bryan Graña Martínez.
