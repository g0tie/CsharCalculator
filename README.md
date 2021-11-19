# CsharpCalculator

Basic Calculator created usign .NET Core, AvaloniaUI on Ubuntu Linux.

![image](https://user-images.githubusercontent.com/56622131/142367547-8f029a20-d2fb-4f76-887a-db9844b43fd6.png)

<h2>Utilisation</h2>

Vous devez entrer une license pour utiliser la calculatrice (J'ai fait une api et une interface web pour ce faire: [Software License Manager](https://github.com/g0tie/SoftwareLicenceManager))
Mais vous pouvez également entrer en mode développeur et bypasser la license en tappant le mot clé "devmode"

![image](https://user-images.githubusercontent.com/56622131/142646341-c5ae5a13-3c90-4e59-989b-55416bcdc71b.png)


<h3>Calcul</h3>
Entrer un calcul puis appuyer sur la touche "=" pour effectuer le calcul

<h3>Inversion</h3>
Pour inverser le signe d'un nombre, entrer ce nombre puis appuyer sur la touche "+/-"

<h3>Suppression</h3>
Pour supprimer un nombre ou signe, utiliser "retour arrière". 
Pour supprimer tout le calcul, utiliser "C"

<h2>Code</h2>

Le template principal se trouve dans ./Views/MainWindow.axaml
Ce projet utilise le MVVM.

<h3>Actions</h3>

A chaque bouton est associé une fonction 

```xaml
 <Button
    Height="50"
    Width="50" 
    Grid.Row="3" Grid.Column="1" 
    Command="{Binding AddCharacterToDisplay}"
    CommandParameter="0"
>0</Button>
```
Ici est associé au bouton, la fonction <b>AddCharacterToDisplay</b>
```xaml
Command="{Binding AddCharacterToDisplay}"
```

Ici est associé un paramètre pour la fonction
```xaml
CommandParameter="0"
```

<h3>Fonctions</h3>

Les fonctions se trouve dans ./ViewModels/MainWindowViewModel.cs

```csharp
 // Gère l'ajout de charactère sur la calculatrice
 public void AddCharacterToDisplay(string character) {}

// Gère l'éxécution du calcul 
public void ExecuteCalculation() {}

//Rafraichit l'écran de la calculatrice
public void ResetCalculation() {}

//Supprime un charactère à la fois (le dernier charactere de l'écran)
public void GoBack() {}

//Inverse le signe du dernier nombre entré
public void InvertSign() {}

//helper pour supprimer les parenthèses d'un string
static string parenthesisRemove(string nb) {}


```
