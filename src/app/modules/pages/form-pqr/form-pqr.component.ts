import { AfterViewInit, Component, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PQRModel } from 'src/app/models/pqr.model';
import { CaseTypeService } from 'src/app/services/case-type.service';
import { CountryService } from 'src/app/services/country.service';
import { PqrService } from 'src/app/services/pqr.service';
import { UserTypeService } from 'src/app/services/user-type.service';
import { EventEmitter } from 'stream';
import Swal from 'sweetalert2';
//se agrego tipo de caso y cedula con metodo de solo digito numerico, no se eliminan los caracteres de cedula
@Component({
  selector: 'app-form-pqr',
  templateUrl: './form-pqr.component.html',
  styleUrls: ['./form-pqr.component.scss']
})
export class FormPqrComponent implements OnInit {

  formPQR: FormGroup;
  // fileToUpload: File | null = null;
  MAX_CARACTERES_COMENTARIO = 1000;
  caracteresRestantes: number = this.MAX_CARACTERES_COMENTARIO;

  listaPais: any[] = [];
  listaTipoCaso: any[] = [];
  listaTipoUsuario: any[] = [];

  public archivos: any = [];
  public selectedFile: any;
  public invalidSize: boolean = false;

  userTypeSelected: number = -1;
  archivoInvalido: boolean | undefined;

  constructor(
    private fb: FormBuilder,
    public router: Router,
    private country: CountryService,
    private caseType: CaseTypeService,
    private userType: UserTypeService,
    private pqrService: PqrService
  ) {
    this.formPQR = this.fb.group({
      tipoCaso: ['', [Validators.required]],
      tipoUsuario: ['', [Validators.required]],
      pais: ['', [Validators.required]],
      razonSocial: ['',],
      nit: ['',],
      cedula: ['',],
      nombre: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      telefono: ['', [Validators.minLength(7), Validators.pattern(/^[0-9]+$/)]],
      comentario: ['', [Validators.required, Validators.maxLength(1000)]],
      file: [''],
      autorizo: [false]
    });

    this.cargarData();
  }

  ngOnInit(): void {
    this.consultarPaises();
    this.consultarTipoCaso();
    this.consultarTipoUsuario();
  }

  actualizarCaracteresRestantes() {
    const comentario = this.formPQR.get('comentario')?.value;
    const longitudComentario = comentario ? comentario.length : 0;
    this.caracteresRestantes = this.MAX_CARACTERES_COMENTARIO - longitudComentario;
    if (this.caracteresRestantes < 0) { // verifica si se ha excedido el límite
      this.formPQR.get('comentario')?.setValue(comentario.substring(0, this.MAX_CARACTERES_COMENTARIO)); // recorta el texto
    }
  }

  verificarTamanioArchivo(event: any) {
    const archivo = event.target.files[0];
    const tamanioMaximo = 2 * 1024 * 1024; // Tamaño máximo en bytes (2 megabytes)

    if (archivo && archivo.size > tamanioMaximo) {
      this.archivoInvalido = true;
      this.formPQR.get('adjunto')?.setValue(null); // despejar el valor del archivo seleccionado
    } else {
      this.archivoInvalido = false;
    }
  }


  get tipoCasoNoValido() {
    return this.formPQR.get('tipoCaso')!.invalid && this.formPQR.get('tipoCaso')!.touched;
  }
  get tipoUsuarioNoValido() {
    return this.formPQR.get('tipoUsuario')!.invalid && this.formPQR.get('tipoUsuario')!.touched;
  }

  get razonSocialNoValida() {
    return this.formPQR.get('razonSocial')!.invalid && this.formPQR.get('razonSocial')!.touched;
  }

  get nitNoValido() {
    return this.formPQR.get('nit')!.invalid && this.formPQR.get('nit')!.touched;
  }

  get cedulaNoValido() {
    return this.formPQR.get('cedula')!.invalid;
  }

  get paisNoValido() {
    return this.formPQR.get('pais')!.invalid && this.formPQR.get('pais')!.touched;
  }
  get nombreNoValido() {
    return this.formPQR.get('nombre')!.invalid && this.formPQR.get('nombre')!.touched;
  }
  get emailNoValido() {
    return this.formPQR.get('email')!.invalid && this.formPQR.get('email')!.touched;
  }
  get telefonoNoValido() {
    return this.formPQR.get('telefono')!.invalid
  }
  get comentarioNoValido() {
    return this.formPQR.get('comentario')!.invalid && this.formPQR.get('comentario')!.touched;
  }

  consultarPaises() {
    this.country.getAll().subscribe((response: any) => {
      console.log('paises: ', response.data);
      this.listaPais = response.data;
      console.log('lista pais', this.listaPais);
    });
  }

  consultarTipoCaso() {
    this.caseType.getAll().subscribe((response: any) => {
      console.log('tipos caso: ', response.data);
      this.listaTipoCaso = response.data;
    });
  }

  consultarTipoUsuario() {
    this.userType.getAll().subscribe((response: any) => {
      console.log('tipos usuario: ', response.data);
      this.listaTipoUsuario = response.data;
    });
  }

  tipoUsuario(event: any) {
    console.log('Recibo esto: ', event.target["selectedIndex"]);

    /*this.formPQR.get('nit')?.clearValidators();
    this.formPQR.get('nit')?.setValidators([Validators.required]);
    this.formPQR.get('razonSocial')?.setValidators([Validators.required]);*/
    //this.userTypeSelected = event.target["selectedIndex"];

    this.formPQR.get('razonSocial')?.clearValidators();
    this.formPQR.get('razonSocial')?.updateValueAndValidity();

    this.formPQR.get('nit')?.clearValidators();
    this.formPQR.get('nit')?.updateValueAndValidity();

    this.formPQR.get('cedula')?.clearValidators();
    this.formPQR.get('cedula')?.updateValueAndValidity();

    if (event.target["selectedIndex"] === 0) {
      this.userTypeSelected = 0;
      // Configurar las validaciones para persona natural
      this.formPQR.get('cedula')?.setValidators([Validators.pattern(/^[0-9]+$/), Validators.minLength(7)]);
      this.formPQR.get('cedula')?.updateValueAndValidity();
    } else if (event.target["selectedIndex"] === 1) {
      this.userTypeSelected = 1;
      // Configurar las validaciones para empresa
      this.formPQR.get('nit')?.setValidators([Validators.pattern(/^[0-9]+$/), Validators.minLength(7), Validators.required]);
      this.formPQR.get('razonSocial')?.setValidators([Validators.required, Validators.maxLength(20)]);
      this.formPQR.get('nit')?.updateValueAndValidity();
      this.formPQR.get('razonSocial')?.updateValueAndValidity();
    }

  }

  validateFormat(event: any) {
    let key;
    if (event.type === 'paste') {
      key = event.clipboardData.getData('text/plain');
    } else {
      key = event.keyCode;
      key = String.fromCharCode(key);
    }
    const regex = /[0-9]|\./;
    if (!regex.test(key)) {
      event.returnValue = false;
      if (event.preventDefault) {
        event.preventDefault();
      }
    }
  }

  cargarData() {
    this.formPQR.setValue({
      tipoCaso: '',
      tipoUsuario: '',
      razonSocial: '',
      nit: '',
      pais: '',
      cedula: '',
      nombre: '',
      email: '',
      telefono: '',
      comentario: '',
      file: '',
      autorizo: false,

    });
  }


  guardar() {
    console.log('Guardar PQR');

    if (this.formPQR.invalid) {

      console.log('Formulario no valido');

      return Object.values(this.formPQR.controls).forEach(control => {

        if (control instanceof FormGroup) {
          Object.values(control.controls).forEach(control => control.markAsTouched());
        } else {
          control.markAsTouched();
        }

      });

    }

    Swal.fire({
      title: 'Espere un momento',
      text: 'Actualizando informacion',
      icon: 'info',
      allowOutsideClick: false
    });
    Swal.showLoading();

    this.pqrService.createPQR(this.formPQR.value.pais, this.formPQR.value.comentario, this.formPQR.value.email, this.formPQR.value.nombre, this.formPQR.value.telefono, this.formPQR.value.razonSocial, this.formPQR.value.tipoCaso, this.formPQR.value.tipoUsuario, this.formPQR.value.nit, this.formPQR.value.cedula, this.formPQR.value.autorizo, this.formPQR.value.file)
      .subscribe(result => {

        Swal.fire({
          title: 'Registro Exitoso',
          text: 'su PQR se ha registrado exitosamente',
          icon: 'success'
        });

        // Limpiamos formulario
        this.formPQR.reset();
        this.router.navigateByUrl('/home');

      });


  }

  public onFilesAdded = (event: any) => {
      this.selectFile(event.target.files);
  }

  public selectFile = ($event: any) => {
        this.formPQR.controls['file'].setValue(this.getImageBase64($event));  
  }

  private getImageBase64 = (image: any, nameInput: string = '') => {
    const imageBase64: any = {
      height: 0,
      timestamp: new Date(),
      uri: '',
      fileName: '',
      data: ''
    };
    const file: File = image[0];
    if (file.size > 30000000 && nameInput === 'video') {
      return '';
    } else if (file.size > 3000000 && nameInput === 'consent') {
      return '';
    } else {
      if (nameInput === 'video') this.invalidSize = false;
      //if (nameInput === 'consent') this.invalidPDFSize = false;
      const myDate = new Date();
      const myReader: FileReader = new FileReader();
      myReader.onloadend = (e) => {
        imageBase64.height = file.size;
        imageBase64.timestamp = new Date('2019-01-06T17:16:40');
        imageBase64.uri = file.name;
        imageBase64.fileName = file.name;
        const result = myReader.result ?? '';
        imageBase64.data = result.toString().split(',')[1] ?? '';
      };
      myReader.readAsDataURL(file);
      return imageBase64;
    }
  }

}
