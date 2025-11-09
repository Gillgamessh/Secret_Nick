## Providers

| Name | Version |
| :-- | :-- |
| terraform | >= 1.5.7 |
| aws | >= 6.0 |

## Resources

| Name | Type |
|------|------|
| [aws_vpc](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/resources/vpc) | resource |
| [aws_subnet](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/resources/subnet) | resource |
| [aws_internet_gateway](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/resources/internet_gateway) | resource |
| [aws_route_table](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/resources/route_table) | resource |
| [aws_route](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/resources/route) | resource |
| [aws_route_table_association](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/resources/route_table_association) | resource |
| [aws_availability_zones](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/data-sources/availability_zone) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="vpc_name"></a> [vpc\_name](#input\_vpc\_name) | VPC name | `string` | n/a | no |
| <a name="vpc_cidr"></a> [vpc\_cidr](#input\_vpc\_cidr) | Your VPC adress block  | `string` | yes | no |
| <a name="subnets"></a> [subnets](#input\_subnets) | List of subnets | `map` | yes | no |

## Outputs

| Name | Description |
|------|-------------|
| <a name="output_vpc_id"></a> [vpc\_id](#output\_vpc\_id) | The ID of your VPC |
| <a name="output_vpc_cidr_block"></a> [vpc\_cidr\_block](#output\_public\_ip) | The EC2 public IP (if possible) |
| <a name="output_vpc_subnet_ids"></a> [vpc\_subnet\_vpc\_ids](#output\_vpc\_subnet\_vpc\_ids) | IDs of created subnets (a list) |
| <a name="output_public_subnet_ids"></a> [vpc\_public\_subnet\_ids](#output\_vpc\_public\_subnet\_ids) | IDs of created public subnets (a list) |
| <a name="output_private_subnet_ids"></a> [vpc\_private_subnet_ids](#output\_vpc\_private\_subnet\_ids) | IDs of created private subnets (a list) |

## Usage

```

module "vpc" {
  source   = "./modules/01-vpc"
  vpc_cidr = var.vpc_cidr
  vpc_name = var.vpc_name
  subnets  = var.subnets
}

```

## Output Example

```
subnet_ids = {
  "subnet0" = "subnet-032ae940391017bed"
  "subnet1" = "subnet-0087baa0641c34171"
  "subnet2" = "subnet-01a9a44de5643ce7d"
  "subnet3" = "subnet-0188dfa28df0628c7"
}
vpc_cidr_block = "10.0.0.0/16"
vpc_id = "vpc-08c37bff8bdc5e6b1"
```