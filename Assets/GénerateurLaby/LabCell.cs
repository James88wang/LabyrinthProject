using UnityEngine;

// Définit une cellule qui servira de base au labyrinthe, composés de multiples cellules
public class LabCell {
    public bool visited = false; // Propriété qui désigne si la cellules est déjà visitée
    public GameObject MurH, MurB, MurD, MurG, Sol; // Chaque Cellule possède un sol et 4 murs
}