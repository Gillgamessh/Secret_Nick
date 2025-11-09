## Requirements

No requirements.

## Providers

| Name | Version |
|------|---------|
| <a name="provider_aws"></a> [aws](#provider\_aws) | n/a |

## Modules

No modules.

## Resources

| Name | Type |
|------|------|
| [aws_db_instance.db](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/resources/db_instance) | resource |
| [aws_db_subnet_group.db_subnet_group](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/resources/db_subnet_group) | resource |
| [aws_subnets.private_subnets](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/data-sources/subnets) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_db_id"></a> [db\_id](#input\_db\_id) | The RDS instance identifier, also used as a 'Name' tag for the DB | `string` | n/a | yes |
| <a name="input_db_storage_size"></a> [db\_storage\_size](#input\_db\_storage\_size) | Allocated storage size in GB for the RDS instance | `number` | `20` | no |
| <a name="input_db_username"></a> [db\_username](#input\_db\_username) | Master username for the database | `string` | n/a | yes |
| <a name="input_db_vpc_sg_ids"></a> [db\_vpc\_sg\_ids](#input\_db\_vpc\_sg\_ids) | List of VPC security group IDs for the DB instance | `list(string)` | n/a | yes |
| <a name="input_tags"></a> [tags](#input\_tags) | Tags to apply to the resources | `map(string)` | `{}` | no |

## Outputs

| Name | Description |
|------|-------------|
| <a name="output_db_endpoint"></a> [db\_endpoint](#output\_db\_endpoint) | The connection endpoint for the database |
| <a name="output_db_instance_arn"></a> [db\_instance\_arn](#output\_db\_instance\_arn) | The ARN of the RDS instance |
| <a name="output_db_instance_id"></a> [db\_instance\_id](#output\_db\_instance\_id) | The RDS instance identifier |
| <a name="output_db_instance_status"></a> [db\_instance\_status](#output\_db\_instance\_status) | The current status of the RDS instance |
| <a name="output_db_port"></a> [db\_port](#output\_db\_port) | The port the database is listening on |