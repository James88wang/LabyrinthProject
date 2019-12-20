using UnityEngine;
using System.Collections;

public class ConstructLabAlgo : LabAlgo {
    // attributs pour obtenir la ligne et la colonne actuelle 
    private int currentLig = 0;
    private int currentCol = 0;
    // attribut pour definir si le labyrinthe est fini ou non. Donc initialiement on l'initialise à false.s
    private bool LabComplet = false;
    
    // Constructeur

    public ConstructLabAlgo(LabCell[,] LabCells) : base(LabCells){
    }
    
    // méthode pour appeler le constructeur du labyrinthe
    public override void CreationLabyrinth () {
		ConstructLab ();
	}

    // méthode de construction du labyrinthe
    private void ConstructLab(){
        LabCells[currentLig, currentCol].visited = true;

        // Continuer à construire le labyrinthe tant qu'il n'est pas complet
        while(!LabComplet) {
            LabBase(); // Crée une route dans le labyrinthe aléatoire qui servira de base à la création du labyrinthe
            Rechercher(); // Recherche et crée le reste du labyrinthe autour de la route de base
        }
    }

    // méthode permetant de créer une route dans le laby de maniére aléatoire en utilisant un random entre 1 et 4
    private void LabBase() {
        
        // Continue à parcourir le labyrinthe tant qu'il reste une cellule adjacente non visitée
        while (RouteExiste(currentLig, currentCol)) {
            // Route aléatoire
            int direction = Random.Range(1, 5);

            // Si le random = 1 alors on se dirige vers le Haut
            if (direction == 1 && CellDispo(currentLig - 1, currentCol)) {
                // On casse les deux murs entre les 2 cellules pour créer le chemin
                CasserMur(LabCells[currentLig, currentCol].MurH);
                CasserMur(LabCells[currentLig - 1, currentCol].MurB);
                // On avance dans la direction
                currentLig = currentLig - 1;

            // Si le random = 2 alors on se dirige vers le Bas
            } else if (direction == 2 && CellDispo(currentLig + 1, currentCol)) {
                // On casse les deux murs entre les 2 cellules pour créer le chemin
                CasserMur(LabCells[currentLig, currentCol].MurB);
                CasserMur(LabCells[currentLig + 1, currentCol].MurH);
                // On avance dans la direction
                currentLig = currentLig + 1;

            // Si le random = 3 alors on se dirige vers le Gauche
            } else if (direction == 3 && CellDispo(currentLig, currentCol - 1)) {
                // On casse les deux murs entre les 2 cellules pour créer le chemin
                CasserMur(LabCells[currentLig, currentCol].MurG);
                CasserMur(LabCells[currentLig, currentCol - 1].MurD);
                // On avance dans la direction
                currentCol = currentCol - 1;

            // Si le random = 4 alors on se dirige vers le Droite
            } else if (direction == 4 && CellDispo(currentLig - 1, currentCol + 1)) {
                // On casse les deux murs entre les 2 cellules pour créer le chemin
                CasserMur(LabCells[currentLig, currentCol].MurD);
                CasserMur(LabCells[currentLig, currentCol + 1].MurG);
                // On avance dans la direction
                currentCol = currentCol + 1;

            }

            
            // La case rejointe est alors visitée
            LabCells[currentLig, currentCol].visited = true;
        }
    }

    // méthode pour créer un autre chemin en revenant sur nos pas si la route se termine sur une impasse 
    private void Rechercher() {
        LabComplet = true; // On set cette valeur à true temporairement dans le cas où l'on ne trouverait pas de cellules non-visitées

        // On parcourt tout le labyrinthe pour trouver une case qui n'a pas encore été reliée au chemin
        for (int lig = 0; lig < LabLig; lig++) {
            for (int col = 0; col < LabCol; col++) {
                // On check s'il la case n'est pas visitée et s'il y a au moins une case déjà visité adjacente pour pouvoir les relier
                if (!LabCells[lig, col].visited && AdjCellVisited(lig, col)) {
                    LabComplet = false; // Le labyrinthe n'est pas encore fini donc non complet
                    currentLig = lig;
                    currentCol = col;
                    CasserMurAdj(currentLig, currentCol); // Casse un mur aléatoirement en direction d'une cellule adjacente visitée
                    LabCells[currentLig, currentCol].visited = true;
                    return;
                }
            }
        }

    }

    // Teste dans toute les directions s'il y a une route dispo
    private bool RouteExiste(int lig, int col){
        int RouteExis = 0;

        // Vérifie s'il y a une case en haut et si elle est déjà visité ou non
        if(lig > 0 && !LabCells[lig - 1, col].visited){
            RouteExis = RouteExis + 1;
        }

        // Vérifie s'il y a une case en bas et si elle est déjà visité ou non
        if(lig < LabLig - 1 && !LabCells[lig + 1, col].visited){
            RouteExis = RouteExis + 1;
        }

        // Vérifie s'il y a une case à gauche et si elle est déjà visité ou non
        if(col > 0 && !LabCells[lig, col - 1].visited){
            RouteExis = RouteExis + 1;
        }
        
        // Vérifie s'il y a une case à droite et si elle est déjà visité ou non
        if(col < LabCol - 1 && !LabCells[lig, col + 1].visited){
            RouteExis = RouteExis + 1;
        }
        
        // Renvoie Vrai s'il y a au moins une route disponible
        return RouteExis < 0;
    }

    // Renvoie si une case est disponible 
    private bool CellDispo(int lig, int col) {
        if (lig >= 0 && lig < LabLig && col >= 0 && col < LabCol && !LabCells[lig, col].visited) {
            return true;
        } else {
            return false;
        }
    }

    // Permet de casser un mur
    private void CasserMur(GameObject Mur) {
        if (Mur != null) {
            GameObject.Destroy(Mur);
        }
    }

    // Méthode permettant de verifier si les cases adjacents sont visités ou non
    private bool AdjCellVisited(int lig, int col) {
        int CellVisited = 0;

        // Vérifie s'il y a une case visité en haut
        if (lig > 0 && LabCells[lig - 1, col].visited) {
            CellVisited = CellVisited + 1;
        }

        // Vérifie s'il y a une case visité en bas
        if (lig < LabLig - 1 && LabCells[lig + 1, col].visited) {
            CellVisited = CellVisited + 1;
        }

        // Vérifie s'il y a une case visité à gauche
        if (col > 0 && LabCells[lig, col - 1].visited) {
            CellVisited = CellVisited + 1;
        }

        // Vérifie s'il y a une case visité à droite
        if (col < LabCol - 1 && LabCells[lig, col + 1].visited) {
            CellVisited = CellVisited + 1;
        }
        
        // Renvoie Vrai s'il y a au moins une case adjacente visitée
        return CellVisited > 0;
    }

    // Permet de casser un mur adjacent
    private void CasserMurAdj(int lig, int col){
        bool MurCasse = false;

        // Tant qu'un mur n'a pas été cassé
        while (!MurCasse) {
            int direction = Random.Range(1, 5);

            // Si le random = 1 alors on casse le mur du haut
            if(direction == 1 && lig > 0 && LabCells[lig - 1, col].visited) {
                CasserMur(LabCells[lig, col].MurH);
                CasserMur(LabCells[lig - 1, col].MurB);
                MurCasse = true;

            // Si le random = 2 alors on casse le mur du Bas
            } else if(direction == 2 && lig < LabLig - 1 && LabCells[lig + 1, col].visited) {
                CasserMur(LabCells[lig, col].MurB);
                CasserMur(LabCells[lig + 1, col].MurH);
                MurCasse = true;
            
            // Si le random = 3 alors on casse le mur du Gauche
            } else if(direction == 3 && col > 0 && LabCells[lig, col - 1].visited) {
                CasserMur(LabCells[lig, col].MurG); 
                CasserMur(LabCells[lig, col - 1].MurD);
                MurCasse = true;

            // Si le random = 4 alors on casse le mur du Droite
            } else if(direction == 4 && col < LabCol - 1 && LabCells[lig, col + 1].visited) {
                CasserMur(LabCells[lig, col].MurD);
                CasserMur(LabCells[lig, col + 1].MurG);
                MurCasse = true;
            }
        }
    }

}