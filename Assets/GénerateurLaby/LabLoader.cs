using UnityEngine;
using System.Collections;
// cette classe hérite du MonoBehaviour afin qu'elle puisse etre assimilé par notre GameObject
// c'est à dire que lorsqu'on cliquera sur le bouton "play", ce sera le Labloader qui va etre compilé
public class LabLoader : MonoBehaviour {
    // attribut correspondant à la ligne et la colonne du labyrinthe
    public int LabLig, LabCol;
    // attribut désignant un mur de taille (8,8) créé à partir de Unity 
    public GameObject Mur;
    public float size = 2f;

    private LabCell[,] LabCells;

    // Fonction appelée à l'initialisation
    void Start() {
        InitLab();
        // instanciation de notre labyrinthe
        LabAlgo Lab = new ConstructLabAlgo(LabCells);
        // Puis on peut appliqué les fonctions créer à partir de cette instance
        Lab.CreationLabyrinth();
    }

    // Fonction appelée à chaque actualisation de frame
    void Update() {
    }

    // Fonction qui initialise le labyrinthe en créeant le tableau de cellules possédant tous un sol et 4 murs
    private void InitLab() {
        // Initialisation du tableau
        LabCells = new LabCell[LabLig, LabCol];

        // On parcourt chaque élément du tableau pour initialiser chaque cellules
        for(int lig = 0; lig < LabLig; lig++) {
            for(int col = 0; col < LabCol; col++){
                // On les initialises en tant que cellules
                LabCells[lig, col] = new LabCell();

                // Ajouter un sol
                LabCells[lig, col].Sol =  Instantiate (Mur, new Vector3 (lig*size, -(size/2f), col*size), Quaternion.identity) as GameObject;
                LabCells[lig, col].Sol.name = "Sol " + lig + "," + col;
				LabCells[lig, col].Sol.transform.Rotate (Vector3.right, 90f);
                
                // Ajouter un mur de gauche si c'est la première colonne
                if (col == 0) {
					LabCells[lig, col].MurG = Instantiate (Mur, new Vector3 (lig*size, 0, (col*size) - (size/2f)), Quaternion.identity) as GameObject;
					LabCells[lig, col].MurG.name = "MurG " + lig + "," + col;
				}
                
                // Ajouter le mur de droite
                LabCells[lig, col].MurD = Instantiate (Mur, new Vector3 (lig*size, 0, (col*size) + (size/2f)), Quaternion.identity) as GameObject;
				LabCells[lig, col].MurD.name = "MurG " + lig + "," + col;

                // Ajouter le mur du haut si c'est la première ligne
                if (lig == 0) {
					LabCells[lig, col].MurH = Instantiate (Mur, new Vector3 ((lig*size) - (size/2f), 0, col*size), Quaternion.identity) as GameObject;
					LabCells[lig, col].MurH.name = "MurH " + lig + "," + col;
					LabCells[lig, col].MurH.transform.Rotate (Vector3.up * 90f);
				}

                // Ajouter le mur du bas
                LabCells[lig, col].MurB = Instantiate (Mur, new Vector3 ((lig*size) + (size/2f), 0, col*size), Quaternion.identity) as GameObject;
				LabCells[lig, col].MurB.name = "MurB " + lig + "," + col;
				LabCells[lig, col].MurB.transform.Rotate (Vector3.up * 90f);
            }
        }
    }
}