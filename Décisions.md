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