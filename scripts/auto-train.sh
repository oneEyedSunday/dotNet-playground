(cd .. && mlnet auto-train --task binary-classification --dataset "${1:-data.csv}" --label-column-name "sentiment" --max-exploration-time="${2:-10000}" -V m)
