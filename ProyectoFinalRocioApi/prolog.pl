run_combinacion_favorita:-
  current_prolog_flag(argv, Argv),
  p(Argv,Arg,_),
  atom_number(Arg, Id),
  abrir_conexion,
  combinacionfavorita(T,B,Id,Y,Z),
  write(T),write(','),write(B),nl,
  cerrar_conexion.

main2:-
  current_prolog_flag(argv, Argv),
  p(Argv,Arg,_),
  atom_number(Arg, Id),
  abrir_conexion,
  lista3(X),
  write(X),nl,
  cerrar_conexion.

p([H|T], H, T).

abrir_conexion:-
    odbc_connect('prolog2',_,
                 [user(root),
                  password('qwerty'),
                  alias(con),
                  open(once)]).
cerrar_conexion:-
    odbc_disconnect('con').

consultar_conexion(F):-
    odbc_query('con',
               'SELECT * FROM clientes',row(F)).
clientes(X,Z):-
    odbc_query('con',
               'SELECT (id_cliente),(nombre_cliente) FROM clientes', row(X,Z)).
bebidas(Y,Z):-
    odbc_query('con',
               'SELECT (id_bebida),(nombre) FROM bebidas',row(Y,Z)).

tamanos(A,B):-
    odbc_query('con',
               'SELECT (id_tamano),(tamano_bebida) FROM tamanos',row(A,B)).
               
historialconsumo(B,C):-
    odbc_query('con','SELECT (id_cliente),(id_bebida) FROM historial_consumo', row(B,C)).

historialconsumo_t(C,T):-
    odbc_query('con','SELECT (id_cliente),(id_tamano) FROM historial_consumo', row(C,T)).

consumio(X,A):-
    bebidas(C,X),
    clientes(B,A),
    historialconsumo(B,C).

cliente_contador(N,A,B) :- findall(A,historialconsumo(A,B),L), length(L,N).

bebida_contador(N,A,B) :- findall(A,bebidas(A,B),L), length(L,N).

cliente_contador_t(N,C,T):- findall(C,historialconsumo_t(C,T),L), length(L,N).

lista(E,Z):-
    bebidas2(ID,CANT), % Saca el id de la bebida
    E is CANT,
    nl.

bebidas2(ID,CANT):-
    odbc_query('con',
               'SELECT (id_bebida) FROM bebidas order by id_bebida ASC',row(ID)),
    cliente_contador(E,C,ID), % Saca el numero de veces que se ha comprado la bebida
    CANT is E.

esmayor(U,E,R):-
    (E > U ->
    R is E;
    E < U ->
    R is U),
    nl.

hacetodo(Z):-
    bebidas(M,N), % Saca el id de la bebida
    cliente_contador(E,C,M), % saca el numero de veces que se ha comprado la bebida
    write('Valor Z Final:'),write(Z),
    Z is R,
    nl.

lista2(M,N):- 
    cliente_contador(E,C,M),
    write('Bebida #'),write(M),write(': '),write(E),
    M<N, 
    nl, 
    NuevoM is M+1,
    lista2(NuevoM,N).

lista4(INI,NUMB,I,R):- 
    cliente_contador(E,C,INI),
    INI=<NUMB,
    NR is E,
    write(INI),write(','),write(NR),nl,
    NuevoM is INI+1,
    lista4(NuevoM,NUMB,NI,NR).

lista3(Z):-
    bebida_contador(P,I,O),
    lista4(1,P,0,R),
    Z is R,write(Z),nl.

bebidanueva(A):-
    bebidas(A,Z),
    cliente_contador(N,B,A),
    N < 5.

bebidafavorita(B,C,Z):-
    bebidas(B,Z),
    clientes(C,Y),
    idcliente_contador(C,N),
    cliente_contador(X,C,B),
    X >= (N/6).
tamanofavorito(T,C,Z):-
    tamanos(T,Z),
    clientes(C,Y),
    idcliente_contador_t(C,N),
    cliente_contador_t(X,C,T),
    X >= (N/6).
combinacionfavorita(T,B,C,Y,Z):-
    tamanofavorito(T,C,Y),
    bebidafavorita(B,C,Z).

idcliente_contador(C,N):-
    clientes(C,Y),
    cliente_contador(N,C,A).
idcliente_contador_t(C,N):-
    clientes(C,Y),
    cliente_contador_t(N,C,T).

bebidamasvendida(BEBIDA,C):-
    bebidas(BEBIDA,NOMBRE),
    cliente_contador(N,CLIENTE,BEBIDA),
    N > C,
    (C is N),
    bebidamasvendida(BEBIDA,C).
    