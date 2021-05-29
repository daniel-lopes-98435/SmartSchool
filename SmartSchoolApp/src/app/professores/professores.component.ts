import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-professores',
  templateUrl: './professores.component.html',
  styleUrls: ['./professores.component.css']
})
export class ProfessoresComponent implements OnInit {

  titulo = "Lista de Professores"

  professores = [
    {nome: 'João'},
    {nome: 'Luan'},
    {nome: 'Cristina'}
  ]
  constructor() { }

  ngOnInit() {
  }

}
