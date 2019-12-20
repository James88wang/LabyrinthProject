using UnityEngine;
using System.Collections;

// Définit le labyrinthe en tant que tableau de cellules, possédant une certaine longueur et hauteur
public abstract class LabAlgo {
    protected LabCell[,] LabCells; // Tableau de cellules
    protected int LabLig, LabCol;  // Longueur et Hauteur du Labyrinthe

    // Initialise le Labyrinthe
    protected LabAlgo(LabCell[,] LabCells) : base(){
        this.LabCells = LabCells;
        LabLig = LabCells.GetLength(0);
        LabCol = LabCells.GetLength(1);
    }
    // créer une methode abstraite qui sera defini dans le ConstructLabAlgo puis appelé dans le LabLoader afin de créer le Labyrinthe
    public abstract void CreationLabyrinth();
}