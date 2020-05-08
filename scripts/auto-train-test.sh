(cd .. && mlnet auto-train --task binary-classification --dataset "${1:-data.csv}" -t "${2}" --label-column-name "sentiment" --max-exploration-time="${3}" -V m)
