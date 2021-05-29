import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-alunos',
  templateUrl: './alunos.component.html',
  styleUrls: ['./alunos.component.css']
})
export class AlunosComponent implements OnInit {

  titulo = 'Lista de alunos'

  public alunos = [
    {nome: 'Juliana'},
    {nome: 'Ana'},
    {nome: 'Bruno'},
    {nome: 'Thainara'}
  ]

  constructor() { }

  ngOnInit() {
  }

}
