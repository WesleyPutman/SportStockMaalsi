## 2026-09-4 — Gabarit

**Contexte** : la situation qui m'obligeait à trancher
**Décision** : ce que j'ai fait
**Alternatives écartées** : et pourquoi
**Conséquences** : ce que ça me coûte, ce que ça m'oblige à faire plus tard

## 2026-09-4 — Réception Silencieuse

**Contexte** : J'ai remarqué des soucis dans le cas où on importe via la fonction AjouterStock(), si on ajoutes un stock négatif, aucune vérification n'est faite avant d'arriver à cette fonction dans le modèle hors, on devrait isoler l'erreur le plus tôt possible et pas attendre la fin. D'autant plus que cela n'affiche pas une erreur sachant que aux yeux du Controller, il va me répondre "OK, stock mis à jour"
**Décision** : AjouterStock va devoir lever une exception pour les erreurs comme une entrée négative ou nulle. Il faut que ce soit plus silencieux mais qu'il le dise.
**Alternatives écartées** : La règle dans le controller serait pas utile car l'erreur pourrait venir d'un autre controller si on fait pas le controle partout. On veut pas se répéter (DRY). Et mettre un booléen n'aurait pas corrigé car ça passerait toujours même si on dit que c'est pas bon.
**Conséquences** : Je vais devoir modifier le controller pour qu'il attrape l'exception que modèle mais surtout faire en sorte qu'un futur possible import CSV ne bloque pas à cause d'une fausse entrée sur 1000

## 2026-09-5 — Gestionnaire D'exceptions - Traduction

**Contexte** : Besoin d'un gestionnaire d'exceptions qui permet de traduire les erreurs métier en réponse HTTP
**Décision** : J'ai choisi d'utiliser un Handler qui renvoit l'erreur avec sa traduction métier en HTTP, ça gère toute les erreurs et j'ai qu'à modifier et ajouter la gestion d'autres erreurs plus tard. Soucis étant qu'il faut préciser qu'on utilise AspNetCore sinon rien n'indique qu'il est la.
**Alternatives écartées** : J'avais pensé à faire le handler dans le controller directement mais ça aurait impliqué de devoir le refaire dans chaque fichier et de faire de la redondance et des oublis. Pareil pour un booléen qui peut juste être ignoré
**Conséquences** : La traduction est invisible quand on lit le controller, un traitement par lot devra attraper l'execution ligne par ligne pour pas se stopper sur une ligne invalide